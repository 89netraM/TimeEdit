using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace TimeEdit
{
	/// <summary>
	/// Represents a <see cref="TimeEdit"/> object that can retrieve schedules
	/// and other information from one specific TimeEdit source.
	/// </summary>
	public class TimeEdit
	{
		/// <summary>
		/// The URL for this <see cref="TimeEdit"/> objects TimeEdit source.
		/// </summary>
		public string BaseURL { get; }

		/// <summary>
		/// Cached value for faster access.
		/// </summary>
		private string searchPageURL;

		/// <summary>
		/// Creates a URL that loads the schedule for the specified course id.
		/// </summary>
		/// <param name="courseId">The id of a course.</param>
		private string CourseURL(int courseId) => $"{BaseURL}ri.json?h=f&sid=3&p=0.m%2C12.n&objects={courseId}&ox=0&types=0&fe=0&h2=f";
		/// <summary>
		/// Creates a URL that loads the search results for the specified query
		/// and filtering types.
		/// </summary>
		/// <param name="searchText">A search query.</param>
		/// <param name="types">Type ids for filtering the search.</param>
		private string SearchURL(string searchText, int types) => $"{BaseURL}objects.html?max=1&fr=t&partajax=t&im=f&sid=3&l=nb_NO&search_text={searchText}&types={types}";

		/// <summary>
		/// Creates a new <see cref="TimeEdit"/> object for retrieving
		/// information from the specified TimeEdit source.
		/// </summary>
		/// <param name="baseURL">
		/// The URL of a TimeEdit source.
		/// <example>
		/// For example: https://cloud.timeedit.net/chalmers/web/public/
		/// </example>
		/// </param>
		public TimeEdit(string baseURL)
		{
			BaseURL = baseURL;
		}

		/// <summary>
		/// Loads the content of a remote URL and returns a
		/// <see cref="XmlReader"/> ready to parse the response.
		/// </summary>
		/// <param name="URL">A URL which content to download.</param>
		private async Task<XmlReader> LoadURL(string URL)
		{
			using (HttpClient client = new HttpClient())
			{
				Stream srcStream = await client.GetStreamAsync(URL);

				return XmlReader.Create(srcStream);
			}
		}

		/// <summary>
		/// Gets (or loads) the URL of the search page.
		/// </summary>
		private async Task<string> GetSearchPageURL()
		{
			if (searchPageURL == null)
			{
				searchPageURL = await LoadSearchpageURL();
			}

			return searchPageURL;
		}
		/// <summary>
		/// Loads the URL of the search page.
		/// </summary>
		private async Task<string> LoadSearchpageURL()
		{
			using (XmlReader reader = await LoadURL(BaseURL))
			{
				// CSS selector ".leftlistcolumn > a:first-of-type"

				try
				{
					bool foundLeftListColumn = reader.ReadToFollowing(r => r.GetAttribute("class")?.Split(" ").Contains("leftlistcolumn") ?? false, XmlNodeType.Element);
					if (!foundLeftListColumn)
					{
						return null;
					}

					bool foundA = reader.ReadToDescendant("a");
					if (!foundA)
					{
						return null;
					}
				}
				catch (XmlException)
				{
					return null;
				}

				string href = reader.GetAttribute("href");

				if (href != null)
				{
					Match result = Regex.Match(href, @"[^/]*\.html$");
					if (result.Success)
					{
						return result.Value;
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Fetches and returns a list of all avalible <see cref="ScheduleType"/>'s.
		/// </summary>
		public async Task<IReadOnlyList<ScheduleType>> GetScheduleTypes()
		{
			string searchPageURL = await GetSearchPageURL();

			List<ScheduleType> scheduleTypes = new List<ScheduleType>();

			using (XmlReader reader = await LoadURL(BaseURL + searchPageURL))
			{
				try
				{
					reader.ReadToFollowing(r => r.GetAttribute("name") == "fancytypeselector", XmlNodeType.Element);
					int optionsDepth = reader.Depth + 1;

					while (reader.Read() && reader.Depth == optionsDepth)
					{
						if (reader.NodeType == XmlNodeType.Element)
						{
							scheduleTypes.Add(
								new ScheduleType(
									reader.ReadContentAsString(),
									int.Parse(reader.GetAttribute("value"))
								)
							);
						}
					}
				}
				catch (XmlException) { }
			}

			return scheduleTypes.AsReadOnly();
		}
	}
}