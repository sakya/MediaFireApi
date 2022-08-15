namespace MediaFireApi.Models
{
    public enum ApiResult
    {
        Success,
        Error
    }

    public enum YesNo
    {
        No,
        Yes
    }

    public enum Order
    {
        /// <summary>
        /// Ascending
        /// </summary>
        Asc,
        /// <summary>
        /// Descending
        /// </summary>
        Desc
    }

    public enum FolderContentType
    {
        Folders,
        Files
    }

    public enum FileType
    {
        Application,
        Archive,
        Audio,
        Development,
        Data,
        Document,
        Image,
        Presentation,
        Spreadsheet,
        Video
    }
    public enum ActionOnDuplicate
    {
        /// <summary>
        /// Ignore the create
        /// </summary>
        Skip,
        /// <summary>
        /// Append a number to the new folder's name
        /// </summary>
        Keep,
        /// <summary>
        /// Preserve the preexisting folder
        /// </summary>
        Replace
    }

    public enum Privacy
    {
        Public,
        Private
    }
}