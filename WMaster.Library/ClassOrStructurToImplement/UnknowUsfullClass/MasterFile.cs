using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMaster.ClassOrStructurToImplement.HFileToImport
{

    public class MasterFile
    {
        private SortedDictionary<string, int> files = new SortedDictionary<string, int>();


        public bool exists(string name)
        {
            return (files.ContainsKey(name));
        }

        public int size()
        {
            return files.Count;
        }

        public void add(string str)
        {
            files[str] = 1;
        }

        public bool LoadXML(IXmlHandle hLoadedFiles)
        {
            files.Clear();
            IXmlElement pLoadedFiles = hLoadedFiles.ToElement();
            if (pLoadedFiles == null)
            {
                return false;
            }

            IXmlElement pGirlsFiles = pLoadedFiles.FirstChildElement("Girls_Files");
            if (pGirlsFiles == null)
            {
                return false;
            }

            for (IXmlElement pFile = pGirlsFiles.FirstChildElement("File"); pFile != null; pFile = pFile.NextSiblingElement("File"))
            {
                if (pFile.Attribute("Filename"))
                {
                    add(pFile.Attribute("Filename"));
                }
            }

            return true;
        }

        public IXmlElement SaveXML(IXmlElement pRoot)
        {
            IXmlElement pLoadedFiles = new IXmlElement("Loaded_Files");
            pRoot.LinkEndChild(pLoadedFiles);
            IXmlElement pGirlsFiles = new IXmlElement("Girls_Files");
            pLoadedFiles.LinkEndChild(pGirlsFiles);

            int numfiles = 0;
            SortedDictionary<string, int>.Enumerator it;
            for (it = files.GetEnumerator(); it.MoveNext(); )
            {
                IXmlElement pFile = new IXmlElement("File");
                pGirlsFiles.LinkEndChild(pFile);
                pFile.SetAttribute("Filename", it.Current.Key);
                numfiles++;
            }
            pLoadedFiles.SetAttribute("NumberofFiles", numfiles);
            return pLoadedFiles;
        }
    }
}
