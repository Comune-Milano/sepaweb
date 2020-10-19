; (function () {
    var telerikDemo = window.telerikDemo = window.telerikDemo || {};
    var NOTIFICATIONS_COUNT = 4;
    var notification;
    var currentNotificationIndex = 0;

    telerikDemo.onClientUpdated = function (sender, args) {
        notification = sender;
        var notificationData = telerikDemo.getNotificationDataFromServer();
        telerikDemo.updateNotificationTitleAndIcon(notificationData);
        if (telerikDemo.isWarningNotification()) {
            telerikDemo.setNotificationShowAndCloseInterval(15000, 12000);
        }
        else {
            telerikDemo.setNotificationShowAndCloseInterval(6500, 3500);
        }
        telerikDemo.sendNextNotificationIndexToServer();
    }

    telerikDemo.getNotificationDataFromServer = function () {
        var notificationData = notification.get_value().split("|");
        return {
            notificationTitle: notificationData[0],
            notificationTitleIconType: notificationData[1]
        }
    }

    telerikDemo.updateNotificationTitleAndIcon = function (notificationData) {

        notification.set_title(notificationData.notificationTitle);
        telerikDemo.showOrHideTitlebarIcon(notificationData.notificationTitleIconType);
    }

    telerikDemo.showOrHideTitlebarIcon = function (notificationTitleIconType) {
        var notificationPopup = notification.get_popupElement();
        var titlebarIconElement = notificationPopup.getElementsByTagName("span")[0];
        titlebarIconElement.style.display = notificationTitleIconType == "none" ? "none" : "";
    }

    telerikDemo.isWarningNotification = function () {
        return currentNotificationIndex == 1;
    }

    telerikDemo.setNotificationShowAndCloseInterval = function (showInterval, closeDelay) {
        notification.set_showInterval(showInterval);
        notification.set_autoCloseDelay(closeDelay);
    };

    telerikDemo.sendNextNotificationIndexToServer = function () {
        var nextNotificationIndex = telerikDemo.getNextNotificationIndex();
        notification.set_value(nextNotificationIndex);
    }

    telerikDemo.getNextNotificationIndex = function () {
        currentNotificationIndex++;
        if (currentNotificationIndex > NOTIFICATIONS_COUNT - 1)
            currentNotificationIndex = 0;
        return currentNotificationIndex;
    }

    telerikDemo.onClientShowing = function (sender, args) {
        notification = sender;
        if (telerikDemo.isWarningNotification()) {
            notification.set_width(560);
        } else {
            notification.set_width(390);
        }
    }


})();