using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using QuickAccessTrail.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAccessTrail.FIlters
{
    public class LinkElementFinder
    {
        private readonly Document _hostDocuent;
        public LinkElementFinder(Document hostDocument)
        {
            _hostDocuent = hostDocument;
        }
        public LinkedElementResult FindLinkedElement(int elementId)
        {
            List<RevitLinkInstance> links = new FilteredElementCollector(_hostDocuent).OfClass(typeof(RevitLinkInstance)).Cast<RevitLinkInstance>().ToList();
            foreach (RevitLinkInstance link in links)
            {
                Document linkedDoc = link.GetLinkDocument();
                if (linkedDoc == null)
                {
                    continue;
                }
                Element linkedElement = linkedDoc.GetElement(new ElementId((long)elementId));
                if (linkedElement != null)
                {
                    return new LinkedElementResult
                    {
                        Element = linkedElement,
                        LinkedDocument = linkedDoc,
                        LinkInstance = link
                    };
                }

            }
            return null;
            
        }
    }
}
