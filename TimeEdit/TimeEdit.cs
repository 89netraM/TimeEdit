using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MoreTec.TimeEditApi
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
		/// Creates a URL that loads the schedule for the specified schedule id.
		/// </summary>
		/// <param name="courseId">The id of a schedule.</param>
		private string ScheduleURL(int courseId) => $"{BaseURL}ri.json?sid=3&objects={courseId}";
		/// <summary>
		/// Creates a URL that loads the search results for the specified query
		/// and filtering types.
		/// </summary>
		/// <param name="searchText">A search query.</param>
		/// <param name="types">Type ids for filtering the search.</param>
		private string SearchURL(string searchText, params int[] types) => $"{BaseURL}objects.json?sid=3&partajax=t&search_text={searchText}&types={String.Join(',', types)}";

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
			using (HttpClient client = new HttpClient())
			{
				Stream srcStream = await client.GetStreamAsync(URL);
				XmlDictionaryReader jsonReader = JsonReaderWriterFactory.CreateJsonReader(srcStream, new XmlDictionaryReaderQuotas());
				return XElement.Load(jsonReader);
			}
		}

		/// <summary>
		/// Fetches and returns a list of all avalible <see cref="ScheduleType"/>'s.
		/// </summary>
		public async Task<IImmutableList<ScheduleType>> GetScheduleTypes()
		{
			XElement json = await LoadURL(TypesURL());

			return json.XPathSelectElement("//records").Elements().Select(x => new ScheduleType(x.XPathSelectElement("name").Value, int.Parse(x.XPathSelectElement("id").Value))).ToImmutableList();
		}

		/// <summary>
		/// Performs a search with filters and returns the results.
		/// </summary>
		/// <param name="query">A search query.</param>
		/// <param name="types">A list of type id's for filtering the results.</param>
		public async Task<IImmutableList<SearchItem>> Search(string query, params int[] types)
		{
			XElement json = await LoadURL(SearchURL(query, types));

			return json.XPathSelectElement("//records").Elements().Select(x => new SearchItem(int.Parse(x.XPathSelectElement("id").Value), int.Parse(x.XPathSelectElement("typeId").Value), x.XPathSelectElement("values").Value)).ToImmutableList();
		}

		/// <summary>
		/// Returns a list of all avalible reservations of the specified ids
		/// schedule.
		/// </summary>
		/// <param name="scheduleId">The id of a schedule.</param>
		public async Task<Schedule> GetSchedule(int scheduleId)
		{
			XElement json = await LoadURL(ScheduleURL(scheduleId));

			IImmutableList<string> columnNames = json.XPathSelectElement("//columnheaders").Elements().Select(x => x.Value).ToImmutableList();

			IImmutableList<ScheduleEntry> entries = json.XPathSelectElement("//reservations").Elements().Select(reservation =>
			{
				string id = reservation.XPathSelectElement("id").Value;
				string startTime = reservation.XPathSelectElement("startdate").Value + "T" + reservation.XPathSelectElement("starttime").Value;
				string endTime = reservation.XPathSelectElement("enddate").Value + "T" + reservation.XPathSelectElement("endtime").Value;

				IImmutableList<string[]> columnValues = reservation.XPathSelectElement("columns").Elements().Select(x => x.Value.Split(", ")).ToImmutableList();
				IImmutableDictionary<string, string[]> columns = columnNames.Zip(columnValues, (k, v) => new { k, v }).ToImmutableDictionary(x => x.k, x => x.v);

				return new ScheduleEntry(id, DateTime.Parse(startTime), DateTime.Parse(endTime), columns);
			}).ToImmutableList();

			return new Schedule(columnNames, entries);
		}
	}
}