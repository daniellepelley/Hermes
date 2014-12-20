using System.Collections.Generic;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data
{
    /// <summary>
    /// A wrapper around a Linq To SQL data context
    /// </summary>
    public class XmlDataContext : IDataContext
    {
        #region Properties

        private string directoryPath;
        /// <summary>
        /// The path of the directory to store the Xml files
        /// </summary>
        public string DirectoryPath
        {
            get { return directoryPath; }
            set { directoryPath = value; }
        }

        private Dictionary<string, string> saveWork = new Dictionary<string, string>();

        public Dictionary<string, string> SaveWork
        {
            get { return saveWork; }
            set { saveWork = value; }
        }

        private Dictionary<string, string> deleteWork = new Dictionary<string, string>();

        public Dictionary<string, string> DeleteWork
        {
            get { return deleteWork; }
            set { deleteWork = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A wrapper around a Linq To SQL data context
        /// </summary>
        public XmlDataContext(string directoryPath)
        {
            this.directoryPath = directoryPath;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Whether there are changes ready to be persisted to the data source
        /// </summary>
        public bool AreChanges()
        {
            return saveWork.Count > 0 ||
                deleteWork.Count > 0;
        }

        /// <summary>
        /// Saves the changes to the data source
        /// </summary>
        public void SaveChanges()
        {
            //if (!System.IO.Directory.Exists(filePath))


            foreach (var unitOfWork in saveWork)
            {
                System.IO.File.WriteAllText(
                    GetFullFilePath(unitOfWork.Key),
                    unitOfWork.Value);
            }

            foreach (var unitOfWork in deleteWork)
            {
                System.IO.File.Delete(
                    GetFullFilePath(unitOfWork.Key));
            }
        }

        private string GetFullFilePath(string partFilePath)
        {
            return string.Format(
                    "{0}.txt",
                    System.IO.Path.Combine(directoryPath, partFilePath));
        }

        #endregion
    }
}
