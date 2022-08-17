namespace MediaFireApi.Models
{
    /// <summary>
    /// API result
    /// </summary>
    public enum ApiResult
    {
        Success,
        Error
    }

    /// <summary>
    /// Yes/No
    /// </summary>
    public enum YesNo
    {
        No,
        Yes
    }

    /// <summary>
    /// Order
    /// </summary>
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

    /// <summary>
    /// Folder content type
    /// </summary>
    public enum FolderContentType
    {
        Folders,
        Files
    }

    /// <summary>
    /// File type
    /// </summary>
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
        Video,
        Other
    }

    /// <summary>
    /// Action on duplicate
    /// </summary>
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

    /// <summary>
    /// Privacy
    /// </summary>
    public enum Privacy
    {
        Public,
        Private
    }
}