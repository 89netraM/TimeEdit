using System.IO;
using System.Net.Http;
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
	}
}