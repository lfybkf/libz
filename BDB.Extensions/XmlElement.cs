using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BDB
{
	public static class XmlElementExtensions
	{
		public static IEnumerable<string> GetChildElementTags(this XmlElement el)
		{
			return el.ChildNodes.OfType<XmlElement>().Select(n => n.LocalName).Distinct();
		}//func

		public static IEnumerable<XmlElement> GetChildElements(this XmlElement el, string tag)
		{
			if (string.IsNullOrWhiteSpace(tag))
			{
				return el.ChildNodes.OfType<XmlElement>();
			}
			else
			{
				return el.ChildNodes.OfType<XmlElement>().Where(n => n.LocalName == tag);
			}
		}//func

		public static List<KeyValuePair<XmlElement, XmlElement>> GetChildElementsAll(this XmlElement el)
		{
			List<KeyValuePair<XmlElement, XmlElement>> Ret = new List<KeyValuePair<XmlElement, XmlElement>>();
			List<XmlElement> Childs = el.GetChildElements(string.Empty).ToList();
			Childs.ForEach(hel => Ret.Add(new KeyValuePair<XmlElement, XmlElement>(el, hel)));
			Childs.ForEach(hel => Ret.AddRange(hel.GetChildElementsAll()));
			return Ret;
		}//func

	}//class
}//ns
