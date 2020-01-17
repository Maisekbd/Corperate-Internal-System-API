"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Employee = /** @class */ (function () {
    function Employee(empId, name, depId, dep, deptName) {
        this.Id = empId;
        this.Name = name;
        this.DepartmentId = depId;
        this.Department = dep;
        this.DepartmentName = deptName;
    }
    Employee.deserialize = function (employeeResponse) {
        return new Employee(employeeResponse.Id, employeeResponse.Name, employeeResponse.DepartmentId, employeeResponse.Department, employeeResponse.Department.Name);
    };
    return Employee;
}());
exports.Employee = Employee;
//# sourceMappingURL=Employee.model.js.map