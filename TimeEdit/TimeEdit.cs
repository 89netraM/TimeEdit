using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

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
		/// Creates a URL that loads the different types of objects.
		/// </summary>
		private string TypesURL() => $"{BaseURL}types.json";
		/// <summary>
		/// Creates a URL that loads the schedule for the specified course id.
		/// </summary>
		/// <param name="courseId">The id of a course.</param>
		private string CourseURL(int courseId) => $"{BaseURL}ri.json?sid=3&objects={courseId}";
		/// <summary>
		/// Creates a URL that loads the search results for the specified query
		/// and filtering types.
		/// </summary>
		/// <param name="searchText">A search query.</param>
		/// <param name="types">Type ids for filtering the search.</param>
		private string SearchURL(string searchText, params int[] types) => $"{BaseURL}sid=3&partajax=t&search_text={searchText}&types={String.Join(',', types)}";

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
		/// <see cref="XElement"/> for parsing the response.
		/// </summary>
		/// <param name="URL">A URL which content to download.</param>
		private async Task<XElement> LoadURL(string URL)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Fetches and returns a list of all avalible <see cref="ScheduleType"/>'s.
		/// </summary>
		public async Task<IReadOnlyList<ScheduleType>> GetScheduleTypes()
		{
			throw new NotImplementedException();
		}
	}
}