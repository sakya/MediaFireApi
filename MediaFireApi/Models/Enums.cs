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
        Asc,
        Desc
    }

    public enum FolderContentType
    {
        Folders,
        Files
    }

    public enum ActionOnDuplicate
    {
        Skip,
        Keep,
        Replace
    }

    public enum Privacy
    {
        Public,
        Private
    }
}