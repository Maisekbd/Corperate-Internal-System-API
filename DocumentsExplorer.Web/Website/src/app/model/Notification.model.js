"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Notification = /** @class */ (function () {
    function Notification(id, title, body, status, dueDate, isOpen, notificationType, decisionId, meetingId) {
        this.Id = id;
        this.Title = title;
        this.Body = body;
        this.status = status,
            this.DecisionId = decisionId;
        this.DueDate = dueDate;
        this.IsOpen = isOpen;
        this.NotificationType = notificationType;
        this.MeetingId = meetingId;
    }
    Notification.deserialize = function (notificationRes) {
        return new Notification(notificationRes.Id, notificationRes.Title, notificationRes.Body, notificationRes.status, notificationRes.DueDate, notificationRes.IsOpen, notificationRes.NotificationType, notificationRes.DecisionId, notificationRes.MeetingId);
    };
    return Notification;
}());
exports.Notification = Notification;
//# sourceMappingURL=Notification.model.js.map