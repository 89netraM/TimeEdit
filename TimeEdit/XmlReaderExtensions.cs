using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TimeEdit
{
	internal static class XmlReaderExtensions
	{
		/// <summary>
		/// Reads until an element matching the predicate is found.
		/// </summary>
		/// <param name="predicate">A predicate that can check the <see cref="XmlReader"/> for a mathcing state.</param>
		/// <returns>Indicates whether the search were successful.</returns>
		internal static bool ReadToFollowing(this XmlReader reader, Predicate<XmlReader> predicate)
		{
			while (reader.Read())
			{
				if (predicate.Invoke(reader))
				{
					return true;
				}
			}

			return false;
		}
		/// <summary>
		/// Reads until an element matching the predicate is found.
		/// </summary>
		/// <param name="predicate">A predicate that can check the <see cref="XmlReader"/> for a mathcing state.</param>
		/// <param name="nodeType">Only runs the predicate against this kind of nods.</param>
		/// <returns>Indicates whether the search were successful.</returns>
		internal static bool ReadToFollowing(this XmlReader reader, Predicate<XmlReader> predicate, XmlNodeType nodeType)
		{
			Predicate<XmlReader> internalPredicate = x => x.NodeType == nodeType && predicate.Invoke(x);
			return reader.ReadToFollowing(internalPredicate);
		}

		/// <summary>
		/// Advances the <see cref="XmlReader"/> to the next descendant element that matches the predicate.
		/// </summary>
		/// <param name="predicate">A predicate that can check the <see cref="XmlReader"/> for a mathcing state.</param>
		/// <returns>Indicates whether the search were successful.</returns>
		internal static bool ReadToDescendant(this XmlReader reader, Predicate<XmlReader> predicate)
		{
			int parentDepth = reader.Depth;
			if (reader.NodeType != XmlNodeType.Element)
			{
				if (reader.ReadState == ReadState.Initial)
				{
					parentDepth--;
				}
				else
				{
					return false;
				}
			}

			while (reader.Read() && reader.Depth > parentDepth)
			{
				if (predicate.Invoke(reader))
				{
					return true;
				}
			}

			return false;
		}
		/// <summary>
		/// Advances the <see cref="XmlReader"/> to the next descendant element that matches the predicate.
		/// </summary>
		/// <param name="predicate">A predicate that can check the <see cref="XmlReader"/> for a mathcing state.</param>
		/// <param name="nodeType">Only runs the predicate against this kind of nods.</param>
		/// <returns>Indicates whether the search were successful.</returns>
		internal static bool ReadToDescendant(this XmlReader reader, Predicate<XmlReader> predicate, XmlNodeType nodeType)
		{
			Predicate<XmlReader> internalPredicate = x => x.NodeType == nodeType && predicate.Invoke(x);
			return reader.ReadToDescendant(internalPredicate);
		}

		/// <summary>
		/// Advances the <see cref="XmlReader"/> to the next sibling element that matches the predicate.
		/// </summary>
		/// <param name="predicate">A predicate that can check the <see cref="XmlReader"/> for a mathcing state.</param>
		/// <returns></returns>
		internal static bool ReadToNextSibling(this XmlReader reader, Predicate<XmlReader> predicate)
		{
			XmlNodeType nt;
			do
			{
				reader.Skip();

				nt = reader.NodeType;
				if (nt == XmlNodeType.Element && predicate.Invoke(reader))
				{
					return true;
				}
			} while (nt != XmlNodeType.EndElement && reader.EOF);

			return false;
		}
	}
}
