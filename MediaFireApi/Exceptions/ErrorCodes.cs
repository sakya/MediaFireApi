namespace MediaFireApi.Exceptions
{
    public enum ErrorCodes
    {
        /// <summary>
        /// Internal server error.
        /// </summary>
        ErrorInternal = 100,
        /// <summary>
        /// User error.
        /// </summary>
        ErrorUserError = 101,
        /// <summary>
        /// API Key is missing.
        /// </summary>
        ErrorMissingKey = 102,
        /// <summary>
        /// The supplied API Key is invalid.
        /// </summary>
        ErrorInvalidKey = 103,
        /// <summary>
        /// Session Token is missing.
        /// </summary>
        ErrorMissingToken = 104,
        /// <summary>
        /// The supplied Session Token is expired or invalid.
        /// </summary>
        ErrorInvalidToken = 105,
        /// <summary>
        /// You cannot change the file extension by changing the file name.
        /// </summary>
        ErrorChangeExtension = 106,
        /// <summary>
        /// The Credentials you entered are invalid.
        /// </summary>
        ErrorInvalidCredentials = 107,
        /// <summary>
        /// Unknown or invalid user.
        /// </summary>
        ErrorInvalidUser = 108,
        /// <summary>
        /// Unknown or invalid Application ID.
        /// </summary>
        ErrorInvalidAppid = 109,
        /// <summary>
        /// Unknown or Invalid QuickKey.
        /// </summary>
        ErrorInvalidQuickkey = 110,
        /// <summary>
        /// Quick Key is missing.
        /// </summary>
        ErrorMissingQuickkey = 111,
        /// <summary>
        /// Unknown or invalid FolderKey.
        /// </summary>
        ErrorInvalidFolderkey = 112,
        /// <summary>
        /// Folder Key is missing.
        /// </summary>
        ErrorMissingFolderkey = 113,
        /// <summary>
        /// Access denied.
        /// </summary>
        ErrorAccessDenied = 114,
        /// <summary>
        /// Cannot move/copy a Folder to itself or to one of its Sub-Folders.
        /// </summary>
        ErrorFolderPathConflict = 115,
        /// <summary>
        /// The date specified is not valid.
        /// </summary>
        ErrorInvalidDate = 116,
        /// <summary>
        /// Folder Name is missing.
        /// </summary>
        ErrorMissingFoldername = 117,
        /// <summary>
        /// The File Name specified is invalid.
        /// </summary>
        ErrorInvalidFilename = 118,
        /// <summary>
        /// You cannot register a Mediafire.com email address.
        /// </summary>
        ErrorNoMfEmail = 119,
        /// <summary>
        /// The email address you specified is already in use.
        /// </summary>
        ErrorEmailTaken = 120,
        /// <summary>
        /// The email address you specified is found to be rejected/bounced.
        /// </summary>
        ErrorEmailRejected = 121,
        /// <summary>
        /// The email address you specified is misformatted.
        /// </summary>
        ErrorEmailMisformatted = 122,
        /// <summary>
        /// The password you specified is misformatted.
        /// </summary>
        ErrorPasswordMisformatted = 123,
        /// <summary>
        /// The API Version is missing.
        /// </summary>
        ErrorApiVersionMissing = 124,
        /// <summary>
        /// The API version specified or the API Library is old for this call. Please Specify a higher API version or update your API Library.
        /// </summary>
        ErrorOldApiVersion = 125,
        /// <summary>
        /// This API call has been deprecated for the API version specified or the API Library. Please refer to the API documentation for alternative calls.
        /// </summary>
        ErrorApiCallDeprecated = 126,
        /// <summary>
        /// The signature you specified is invalid.
        /// </summary>
        ErrorInvalidSignature = 127,
        /// <summary>
        /// Required parameters for this request are missing.
        /// </summary>
        ErrorMissingParams = 128,
        /// <summary>
        /// One or more parameters for this request are invalid.
        /// </summary>
        ErrorInvalidParams = 129,
        /// <summary>
        /// Non premium account limitation reached.
        /// </summary>
        ErrorNonProLimitReached = 130,
        /// <summary>
        /// Cannot add a shared folder to its owner's account.
        /// </summary>
        ErrorAddOwnedFolder = 131,
        /// <summary>
        /// Cannot remove a shared folder from its owner's account.
        /// </summary>
        ErrorRemoveOwnedFolder = 132,
        /// <summary>
        /// Cannot add a shared folder from an anonymous account.
        /// </summary>
        ErrorAddAnonFolder = 133,
        /// <summary>
        /// This contact already exists in the user's contact list.
        /// </summary>
        ErrorContactAlreadyExists = 137,
        /// <summary>
        /// This contact does not exist.
        /// </summary>
        ErrorContactDoesNotExist = 138,
        /// <summary>
        /// This group already exists in the user's contact list.
        /// </summary>
        ErrorContactGroupExists = 139,
        /// <summary>
        /// This group does not exist.
        /// </summary>
        ErrorUnknownContactGroup = 140,
        /// <summary>
        /// Unknown or invalid device.
        /// </summary>
        ErrorUnknownDevice = 141,
        /// <summary>
        /// Unsupported or invalid file type.
        /// </summary>
        ErrorInvalidFileType = 142,
        /// <summary>
        /// This file already exists.
        /// </summary>
        ErrorFileAlreadyExists = 143,
        /// <summary>
        /// This folder already exists.
        /// </summary>
        ErrorFolderAlreadyExists = 144,
        /// <summary>
        /// The application trying to access the API is disabled.
        /// </summary>
        ErrorApplicationDisabled = 145,
        /// <summary>
        /// The application trying to access the API is suspended.
        /// </summary>
        ErrorApplicationSuspended = 146,
        /// <summary>
        /// Bulk downloading from multiple file owners is currently not supported.
        /// </summary>
        ErrorZipMultipleOwners = 147,
        /// <summary>
        /// Bulk download requires the file owner or the downloader to be premium.
        /// </summary>
        ErrorZipNonProDownload = 148,
        /// <summary>
        /// The owner of the files is not a premium user. You need to confirm the download using your own bandwidth.
        /// </summary>
        ErrorZipOwnerNotPro = 149,
        /// <summary>
        /// One or more files are too large to be included. Files must be X MB or less in order to be included in the zip file.
        /// </summary>
        ErrorZipFileTooBig = 150,
        /// <summary>
        /// The item you selected contained no files. You must select at least one file to zip.
        /// </summary>
        ErrorZipNoFilesSelected = 151,
        /// <summary>
        /// None of the selected files were able to be zipped at this time.
        /// </summary>
        ErrorZipNoFilesZipped = 152,
        /// <summary>
        /// The total size of the zip file is larger than X MB. You need to confirm the download.
        /// </summary>
        ErrorZipTotalSizeTooBig = 153,
        /// <summary>
        /// Maximum number of files reached. Cannot add more than X files.
        /// </summary>
        ErrorZipNumFilesExceeded = 154,
        /// <summary>
        /// The files owner does not have enough bandwidth to download the zip file. You need to confirm the download using your own bandwidth.
        /// </summary>
        ErrorZipOwnerInsufficientBandwidth = 155,
        /// <summary>
        /// You do not have enough bandwidth to download the zip file.
        /// </summary>
        ErrorZipRequesterInsufficientBandwidth = 156,
        /// <summary>
        /// Neither the owner of the files nor you have enough bandwidth to download the zip file.
        /// </summary>
        ErrorZipAllInsufficientBandwidth = 157,
        /// <summary>
        /// This file exists already.
        /// </summary>
        ErrorFileExists = 158,
        /// <summary>
        /// This folder exists already.
        /// </summary>
        ErrorFolderExists = 159,
        /// <summary>
        /// The Terms of Service acceptance token is invalid.
        /// </summary>
        ErrorInvalidAcceptanceToken = 160,
        /// <summary>
        /// You must accept the latest Terms of Service.
        /// </summary>
        ErrorUserMustAcceptTos = 161,
        /// <summary>
        /// The file(s)/folder(s) you upload/copy exceed your total storage limit.
        /// </summary>
        ErrorLimitExceeded = 162,
        /// <summary>
        /// You have reached the limit accessing the API. Please try again later.
        /// </summary>
        ErrorAccessLimitReached = 163,
        /// <summary>
        /// These files have already been reported.
        /// </summary>
        ErrorDmcaAlreadyReported = 164,
        /// <summary>
        /// These files no longer exist in our system.
        /// </summary>
        ErrorDmcaAlreadyRemoved = 165,
        /// <summary>
        /// Cannot add a private folder to an account.
        /// </summary>
        ErrorAddPrivateFolder = 166,
        /// <summary>
        /// Maximum depth of folder reached. Cannot add more than X nested folders.
        /// </summary>
        ErrorFolderDepthLimit = 167,
        /// <summary>
        /// Invalid Product Id.
        /// </summary>
        ErrorInvalidProductId = 168,
        /// <summary>
        /// Upload Failed.
        /// </summary>
        ErrorUploadFailed = 169,
        /// <summary>
        /// Can't change plan to one that is not in the same class with the current.
        /// </summary>
        ErrorTargetPlanNotInTheSameClass = 170,
        /// <summary>
        /// Can't change plan from/to business plan.
        /// </summary>
        ErrorBizPlanRestriction = 171,
        /// <summary>
        /// Can't change plan, plan will be expiring or it has already expired.
        /// </summary>
        ErrorExpirationDateRestriction = 172,
        /// <summary>
        /// Must be a premium user to use this function.
        /// </summary>
        ErrorNotPremiumUser = 173,
        /// <summary>
        /// The URL specified is invalid.
        /// </summary>
        ErrorInvalidUrl = 174,
        /// <summary>
        /// The Upload Key specified is invalid.
        /// </summary>
        ErrorInvalidUploadKey = 175,
        /// <summary>
        /// The storage amount for this product is less than the total size of your files.
        /// </summary>
        ErrorStorageLimitRestriction = 176,
        /// <summary>
        /// Cannot insert a duplicate entry.
        /// </summary>
        ErrorDuplicateEntry = 177,
        /// <summary>
        /// Cannot change to same plan.
        /// </summary>
        ErrorProductIdMatch = 178,
        /// <summary>
        /// Must change to a current product.
        /// </summary>
        ErrorNotCurrentProduct = 179,
        /// <summary>
        /// Cannot downgrade from a business account.
        /// </summary>
        ErrorBizDowngrade = 180,
        /// <summary>
        /// Error upgrading to business account.
        /// </summary>
        ErrorBusinessUpgrade = 181,
        /// <summary>
        /// You do not have enough credit to change to this plan. Please contact customer service.
        /// </summary>
        ErrorChangePlanCredit = 182,
        /// <summary>
        /// Changing to this product would give you negative bandwidth.
        /// </summary>
        ErrorBandwidthError = 183,
        /// <summary>
        /// The account you are trying to link is already linked to another MediaFire user.
        /// </summary>
        ErrorAlreadyLinked = 184,
        /// <summary>
        /// The specified Folder Name is invalid.
        /// </summary>
        ErrorInvalidFoldername = 185,
        /// <summary>
        /// Cannot download password-protected files in bulk.
        /// </summary>
        ErrorZipPasswordBulk = 186,
        /// <summary>
        /// Found no server matching your request.
        /// </summary>
        ErrorServerNotFound = 187,
        /// <summary>
        /// You must be logged in to purchase a plan.
        /// </summary>
        ErrorNotLoggedIn = 188,
        /// <summary>
        /// You must agree to the reseller terms of service.
        /// </summary>
        ErrorResellerTos = 189,
        /// <summary>
        /// Business seats cannot make purchases.
        /// </summary>
        ErrorBusinessSeat = 190,
        /// <summary>
        /// This user is a banned buyer.
        /// </summary>
        ErrorBannedBuyer = 191,
        /// <summary>
        /// Error with reseller credits.
        /// </summary>
        ErrorResellerCreditsError = 192,
        /// <summary>
        /// You may not purchase from this country.
        /// </summary>
        ErrorPurchaseBannedError = 193,
        /// <summary>
        /// The subdomain is in use or invalid.
        /// </summary>
        ErrorSubdomainError = 194,
        /// <summary>
        /// This user has too many failed transactions.
        /// </summary>
        ErrorTooManyFailed = 195,
        /// <summary>
        /// The credit card you have entered is invalid.
        /// </summary>
        ErrorInvalidCard = 196,
        /// <summary>
        /// You have purchased an account within the last 3 days.
        /// </summary>
        ErrorRecentSubscriber = 197,
        /// <summary>
        /// There was an error storing the invoice.
        /// </summary>
        ErrorInvoiceFailed = 198,
        /// <summary>
        /// A duplicate transaction has been submitted.
        /// </summary>
        ErrorDuplicateApiTransaction = 199,
        /// <summary>
        /// Invalid card CCV code
        /// </summary>
        ErrorCardccvError = 200,
        InvalidCardCcvCode = 200,
        /// <summary>
        /// This transaction has been declined.
        /// </summary>
        ErrorTransactionDeclined = 201,
        /// <summary>
        /// Prepaid card error.
        /// </summary>
        ErrorPrepaidCard = 202,
        /// <summary>
        /// There was an error storing the credit card.
        /// </summary>
        ErrorCardStoreFailed = 206,
        /// <summary>
        /// Total number of files copied cannot exceed "(MAX_OBJECTS, HTTP_STATUS_FORBIDDEN).
        /// </summary>
        ErrorCopyLimitExceeded = 207,
        /// <summary>
        /// Another Asynchronous Operation is in progress. Please Retry later.
        /// </summary>
        ErrorAsyncJobInProgress = 208,
        /// <summary>
        /// This folder has already been deleted.
        /// </summary>
        ErrorFolderAlreadyDeleted = 209,
        /// <summary>
        /// This file has already been deleted.
        /// </summary>
        ErrorFileAlreadyDeleted = 210,
        /// <summary>
        /// Items in the Trash Can cannot be modified.
        /// </summary>
        ErrorCantModifyDeletedItems = 211,
        /// <summary>
        /// You cannot change from a free plan.
        /// </summary>
        ErrorChangeFromFree = 212,
        /// <summary>
        /// The specified FileDrop Key is invalid.
        /// </summary>
        ErrorInvalidFiledropKey = 214,
        /// <summary>
        /// The call signature is missing.
        /// </summary>
        ErrorMissingSignature = 215,
        /// <summary>
        /// The email address provided must be greater than 3 characters.
        /// </summary>
        ErrorEmailAddressTooShort = 216,
        /// <summary>
        /// The email address provided must be less than 50 characters.
        /// </summary>
        ErrorEmailAddressTooLong = 217,
        /// <summary>
        /// Cannot register via Facebook. The Facebook Email is missing.
        /// </summary>
        ErrorFbEmailMissing = 218,
        /// <summary>
        /// The Facebook Email is already registered with a MediaFire account.
        /// </summary>
        ErrorFbEmailExists = 219,
        /// <summary>
        /// Failed to authenticate to Facebook.
        /// </summary>
        ErrorAuthFacebook = 220,
        /// <summary>
        /// Failed to authenticate to Twitter.
        /// </summary>
        ErrorAuthTwitter = 221,
        /// <summary>
        /// The revision you requested is invalid or cannot be restored.
        /// </summary>
        ErrorInvalidRevision = 223,
        /// <summary>
        /// There is no active invoice to cancel.
        /// </summary>
        ErrorNoActiveInvoice = 224,
        /// <summary>
        /// This application is not allowed to log to the database.
        /// </summary>
        ErrorApplicationNoLogging = 225,
        /// <summary>
        /// Invalid installation ID.
        /// </summary>
        ErrorInvalidInstallationId = 226,
        /// <summary>
        /// The provided incident and installation ID's do not match.
        /// </summary>
        ErrorIncidentMismatch = 227,
        /// <summary>
        /// The Facebook Access Token is required.
        /// </summary>
        ErrorMissingFacebookToken = 228,
        /// <summary>
        /// The Twitter OAuth Token is required.
        /// </summary>
        ErrorMissingTwitterToken = 229,
        /// <summary>
        /// This user has no associated avatar image.
        /// </summary>
        ErrorNoAvatar = 230,
        /// <summary>
        /// The provided software token is invalid.
        /// </summary>
        ErrorInvalidSoftwareToken = 231,
        /// <summary>
        /// The email address of the sender is not yet validated.
        /// </summary>
        ErrorEmailNotValidated = 232,
        /// <summary>
        /// Failed to authenticate to Google.
        /// </summary>
        ErrorAuthGmail = 233,
        /// <summary>
        /// Failed to send message.
        /// </summary>
        ErrorFailedToSendMessage = 234,
        /// <summary>
        /// You own this resource.
        /// </summary>
        ErrorUserIsOwner = 235,
        /// <summary>
        /// You already follow this resource.
        /// </summary>
        ErrorUserIsFollower = 236,
        /// <summary>
        /// You are not following this resource.
        /// </summary>
        ErrorUserNotFollower = 237,
        /// <summary>
        /// This file has not changed; no need to update.
        /// </summary>
        ErrorUpdateNoChange = 238,
        /// <summary>
        /// Maximum number of allowed share for this resource is reached.
        /// </summary>
        ErrorShareLimitReached = 239,
        /// <summary>
        /// Cannot grant permissions to the specified resource(s).
        /// </summary>
        ErrorCannotGrantPerms = 240,
        /// <summary>
        /// The service number provided is not a recognized service.
        /// </summary>
        ErrorInvalidPrintService = 241,
        /// <summary>
        /// The folder trying to be deleted has over 1000 files.
        /// </summary>
        ErrorFolderFilesExceeded = 242,
        /// <summary>
        /// This account is temporarily locked. Please, try again later.
        /// </summary>
        ErrorAccountTemporarilyLocked = 243,
        /// <summary>
        /// This service is available to US residents only.
        /// </summary>
        ErrorNonUsUser = 244,
        /// <summary>
        /// You do not have permissions to access this Service.
        /// </summary>
        ErrorInvalidService = 245,
        /// <summary>
        /// You cannot change from a plan purchased through an affiliate.
        /// </summary>
        ErrorChangeFromAffiliate = 246,
        /// <summary>
        /// You cannot change from a plan purchased through the App Store.
        /// </summary>
        ErrorChangeFromApple = 247,
        /// <summary>
        /// Unable to authenticate app.
        /// </summary>
        ErrorAppNotAuthenticated = 248,
        /// <summary>
        /// Invalid receipt data submitted.
        /// </summary>
        ErrorInvalidReceiptData = 249,
        /// <summary>
        /// Transaction ID not found in receipt data.
        /// </summary>
        ErrorInvalidTransactionId = 250,
        /// <summary>
        /// This transaction ID has already been redeemed.
        /// </summary>
        ErrorUsedTransactionId = 251,
        /// <summary>
        /// The passed session token is already an upgraded version.
        /// </summary>
        ErrorTokenAlreadyUpgraded = 252,
        /// <summary>
        /// Unknown or invalid API method.
        /// </summary>
        ErrorUnknownApi = 253,
        /// <summary>
        /// This Meta List name already exists.
        /// </summary>
        ErrorListAlreadyExists = 254,
        /// <summary>
        /// Unknown or Invalid Meta List.
        /// </summary>
        ErrorUnkownList = 255,
        /// <summary>
        /// Invalid import service.
        /// </summary>
        ErrorInvalidImportService = 256,
        /// <summary>
        /// This client does not have permission to reuse credit card information.
        /// </summary>
        ErrorCardReuseForbidden = 257,
        /// <summary>
        /// The number of files specified exceeds the limit for this print service.
        /// </summary>
        ErrorOverPrintLimit = 258,
        /// <summary>
        /// Folder path not found.
        /// </summary>
        ErrorInvalidFolderPath = 259,
        /// <summary>
        /// File path not found.
        /// </summary>
        ErrorInvalidFilePath = 260,
        /// <summary>
        /// Maximum number of allowed calls for this resource is reached in a specified time.
        /// </summary>
        ErrorRateLimit = 261,
        /// <summary>
        /// Invalid file extension. The only extension currently supported is '.txt'.
        /// </summary>
        ErrorInvalidFileExtension = 262,
        /// <summary>
        /// File extension is missing from file name.
        /// </summary>
        ErrorMissingFileExtension = 263,
        /// <summary>
        /// Hash is missing.
        /// </summary>
        ErrorMissingHash = 264,
        /// <summary>
        /// Unknown or invalid Hash.
        /// </summary>
        ErrorInvalidHash = 265,
        /// <summary>
        /// Device ID is missing.
        /// </summary>
        ErrorMissingDeviceId = 266,
        /// <summary>
        /// Unknown or invalid Device ID.
        /// </summary>
        ErrorInvalidDeviceId = 267,
        /// <summary>
        /// The SHA256 hash of the chunk (x-unit-hash) does not match the SHA256 hash of the payload received by the server.
        /// </summary>
        ErrorUploadFailedHash = 304,
    }
}