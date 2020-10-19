(function(global, undefined) {
    var notificationTimeout = 60;
    var sessionTimeout = 60;
    var second = 1000;
    var seconds = sessionTimeout;
    var secondsBeforeShow = notificationTimeout;
    var mainLabel;
    var notification;
    var demo = {};
    var timers = {};
 
    function pageLoad(sender) {
        mainLabel = $get("mainLbl");
        notification = $find(demo.notificationID);
        var xmlPanel = notification._xmlPanel;
 
        xmlPanel.set_enableClientScriptEvaluation(true);
        resetTimer("mainLblCounter", updateMainLabel);
    };
 
    function notification_showing(sender, args) {
        mainLabel.innerHTML = 0;
        resetTimer("timeLeftCounter", updateTimeLabel);
        stopTimer("mainLblCounter");
    }
 
    function notification_hidden() {
        updateMainLabel(true);
        mainLabel.style.display = "";
        resetTimer("mainLblCounter", updateMainLabel);
    }
 
    function updateMainLabel(toReset) {
        secondsBeforeShow = toReset ? notificationTimeout : secondsBeforeShow - 1;
        mainLabel.innerHTML = secondsBeforeShow;
    }
 
    //update the text in the label in RadNotification
    //this could also be done automatically by using UpdateInterval. However, this will cause callbacks [which is the second best solution than javascript] on every second that is being count
    function updateTimeLabel() {
        var sessionExpired = (seconds === 0);
        if (sessionExpired) {
            stopTimer("timeLeftCounter");
            expireSession();
        }
        else {
            var timeLbl = $get("timeLbl");
            timeLbl.innerHTML = seconds--;
        }
    }
 
    function ContinueSession() {
        //we need to contact the server to restart the Session - the fastest way is via callback
        //calling update() automatically performs the callback, no need for any additional code or control
        notification.update();
        notification.hide();
 
        //resets the showInterval for the scenario where the Notification is not disposed (e.g. an AJAX request is made)
        //You need to inject a call to the ContinueSession() function from the code behind in such a request
        var showIntervalStorage = notification.get_showInterval(); //store the original value
        notification.set_showInterval(0); //change the timer to avoid untimely showing, 0 disables automatic showing
        notification.set_showInterval(showIntervalStorage); //sets back the original interval which will start counting from its full value again
 
        stopTimer("timeLeftCounter");
        seconds = sessionTimeout;
        updateMainLabel(true);
    }
 
    function expireSession() {
        global.location.href = notification.get_value();
    }
 
    function stopTimer(timer) {
        global.clearInterval(timers[timer]);
        delete timers[timer];
    };
 
    function resetTimer(timer, func) {
        var delegate = Function.createDelegate(this, func);
        stopTimer(timer);
        timers[timer] = global.setInterval(delegate, second);
    };
 
    function serverID(name, id) {
        demo[name] = id;
    }
 
    function serverIDs(obj) {
        for(var name in obj) {
            serverID(name, obj[name]);
        }
    }
 
    global.serverIDs = serverIDs;
    global.ContinueSession = ContinueSession;
    global.notification_hidden = notification_hidden;
    global.notification_showing = notification_showing;
 
    Sys.Application.add_load(pageLoad);
})(window);