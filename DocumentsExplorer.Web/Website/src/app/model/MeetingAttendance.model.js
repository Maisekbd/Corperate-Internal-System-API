"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var MeetingAttendance = /** @class */ (function () {
    function MeetingAttendance(id, name, email, jobDesc, empId, empName, companyName, isAttend, memeberType, councilMemberId, councilMemeber, role, causeOfAbs, meetingId, meeting, departmentId, departmentName) {
        this.Id = id;
        this.Name = name;
        this.Email = email;
        this.JobDescription = jobDesc;
        this.EmployeId = empId;
        this.EmployeName = empName;
        this.CompanyName = companyName;
        this.IsAttendant = isAttend;
        this.MemberType = memeberType;
        this.CouncilMemberId = councilMemberId;
        this.CouncilMember = councilMemeber;
        this.Role = role;
        this.CauseOfAbsence = causeOfAbs;
        this.MeetingId = meetingId;
        this.Meeting = meeting;
        this.DepartmentId = departmentId;
        this.DepartmentName = departmentName;
    }
    MeetingAttendance.deserialize = function (attRes) {
        return new MeetingAttendance(attRes.Id, attRes.name, attRes.Email, attRes.JobDescription, attRes.EmployeId, attRes.EmployeName, attRes.CompanyName, attRes.IsAttendant, attRes.MemberType, attRes.CouncilMemberId, attRes.CouncilMember, attRes.Role, attRes.CauseOfAbsence, attRes.MeetingId, attRes.Meeting, attRes.DepartmentId, attRes.DepartmentName);
    };
    MeetingAttendance.getMeetingAttendanceDefault = function () {
        return new MeetingAttendance(0, "", "", "", "", "", "", null, 0, 0, null, null, "", 0, null, 0, "");
    };
    return MeetingAttendance;
}());
exports.MeetingAttendance = MeetingAttendance;
//# sourceMappingURL=MeetingAttendance.model.js.map