// JavaScript source code
function InitCopyFile(destinationLibrary, itemId, sourceUrlClientControlId, sourceLibrary,refeashButtonId) {

    var _ctx;
    var _sourceFile;
    var _destinationlibUrl;
    var _destinationFile;
    var _sourceFileUrl;
    var _curWeb;

    // Get the current client context of the client-side object model (CSOM) runtime.
    _ctx = new SP.ClientContext.get_current();

    // Get the Web site that is associated with the client context.
    _curWeb = _ctx.get_web();
    _ctx.load(_curWeb);
    _ctx.executeQueryAsync(onLoadWebSuccess, onQueryFailed);

    function onLoadWebSuccess(sender, args) {
        //Create a object which will be used for the Asset. The name can anything. I have used 'testAssetPickerObj'
        with (new AssetPickerConfig('MRDueDiligenseAssetPickerObj')) {
            {
                DefaultAssetLocation = _curWeb.get_serverRelativeUrl() + '/' + sourceLibrary;
                //CurrentWebBaseUrl will be the url of the site or site collection. My site comllection url is as follows.
                CurrentWebBaseUrl = _curWeb.get_serverRelativeUrl() ;
                OverrideDialogFeatures = '';
                OverrideDialogTitle = '';
                OverrideDialogDesc = '';
                OverrideDialogImageUrl = '';
                //AssetUrlClientID is the id of the control in which the path of the selected file will be saved. I am saving the path in a text box. And the id is txtURL.
                AssetUrlClientID = sourceUrlClientControlId;
                AssetTextClientID = '';
                UseImageAssetPicker = false; //make this false to show Documents instead
                DefaultToLastUsedLocation = true;
                DisplayLookInSection = true;
                ReturnCallback = fnStartCopyFile;
                
            }
        }


        //function fnGetFileName() {
        //Following is the function which launch the AssetPortalBrowser.aspx automatically.
        APD_LaunchAssetPickerUseConfigCurrentUrl('MRDueDiligenseAssetPickerObj');
    }

//return false;
    // }

    function fnStartCopyFile() {
        //Checking the text box value, where the url of the selected file is getting saved.
        // alert($('#txtURL').val());
        _sourceFileUrl =jQuery('#' + sourceUrlClientControlId).val();
        CopyFile();
        
    }


    function CopyFile() {

        // Get the file that is represented by the item from a document library.
        _sourceFile = _curWeb.getFileByServerRelativeUrl(_sourceFileUrl);
        _ctx.load(_sourceFile);

        _ctx.executeQueryAsync(Function.createDelegate(this, onQuerySucceeded),
            Function.createDelegate(this, onQueryFailed));
    }

    // Delegate that is called when the query completes successfully.
    function onQuerySucceeded(sender, args) {
        if (_sourceFile != null) {
            _destinationlibUrl = _curWeb.get_serverRelativeUrl() +
                '/' + destinationLibrary + '/' + itemId + '/' +
                _sourceFile.get_name();

            _destinationFile = _curWeb.getFileByServerRelativeUrl(_destinationlibUrl);
            _ctx.load(_destinationFile);

            _ctx.executeQueryAsync(Function.createDelegate(this, onLoadSucceeded),
                Function.createDelegate(this, onQueryDesfileFail));
        }
    }

    // Delegate that is called when the destination file checkout completes successfully.
    function onLoadSucceeded(sender, args) {
        if (_destinationFile.get_exists() == true) {
            _destinationFile.checkOut();
        }

        _sourceFile.copyTo(_destinationlibUrl, true);
        notifyId = SP.UI.Notify.addNotification('Copying file... ' +
            _sourceFile.get_serverRelativeUrl() +
            ' to ' + _destinationlibUrl, true);

        _ctx.executeQueryAsync(
            function(sender, args) {
                SP.UI.Notify.removeNotification(notifyId);
                SP.UI.Notify.addNotification('File copied successfully', false);
                jQuery("#" + refeashButtonId).click();
                //window.location.href = window.location.href;
                //if (_destinationFile.get_exists() == true) {
                //_destinationFile.checkIn('Document Check-In',SP.CheckinType.majorCheckIn);
                //}

                // Redirect to Library B when complete
                // window.location = web.get_serverRelativeUrl() + '/LibraryB/';
            },
            function(sender, args) {
                SP.UI.Notify.addNotification('Error copying file', false);
                SP.UI.Notify.removeNotification(notifyId);
                alert('Error occured: ' + args.get_message());
            }
        );
    }

    // Delegate that is called when server operation is completed with errors.
    function onQueryFailed(sender, args) {
        alert('Request failed. ' + args.get_message() + '\n' + args.get_stackTrace());
    }

    function onQueryDesfileFail(sender, args) {
        _sourceFile.copyTo(_destinationlibUrl, true);
        notifyId = SP.UI.Notify.addNotification('Copying file... ' +
            _sourceFile.get_serverRelativeUrl() +
            ' to ' + _destinationlibUrl, true);

        _ctx.executeQueryAsync(
            function(sender, args) {
                SP.UI.Notify.removeNotification(notifyId);
                SP.UI.Notify.addNotification('File copied successfully', false);
                jQuery("#" + refeashButtonId).click();
                //if (_destinationFile.get_exists() == true) {
                //_destinationFile.checkIn('Document Check-In',SP.CheckinType.majorCheckIn);
                //}

                // Redirect to Library B when complete
                // window.location = web.get_serverRelativeUrl() + '/LibraryB/';
            },
            function(sender, args) {
                SP.UI.Notify.addNotification('Error copying file', false);
                SP.UI.Notify.removeNotification(notifyId);
                alert('Error occured: ' + args.get_message());
            }
        );

    }

}