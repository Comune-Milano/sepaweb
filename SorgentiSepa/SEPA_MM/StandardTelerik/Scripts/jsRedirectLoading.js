var orgOpen = window.open;
window.open = function (url, windowName, windowFeatures) {
    var prosegui = true;
    if (document.getElementById('HFForceNoLoadingPanel')) {
        if (document.getElementById('HFForceNoLoadingPanel').value == 1) {
            prosegui = false
        };
    };
    if (prosegui == true) {
        var name = windowName = "ZZ" + windowName + "ZZ";
        var Page = '';
        if (document.getElementById('HFPathExit')) {
            Page = '../SERVICES/RedirectLoading.html';
        } else {
            Page = 'SERVICES/RedirectLoading.html';
        };
        var w0 = orgOpen(Page, name, windowFeatures);
        window.setTimeout(function () {
            var w1 = orgOpen(url, name, windowFeatures);
        }, 500);
    } else {
        orgOpen(url, windowName, windowFeatures);
        if (document.getElementById('HFForceNoLoadingPanel')) {
            document.getElementById('HFForceNoLoadingPanel').value = 0;
        };
    };
};