import { Deserializable } from "../model/deserializable.model";
import { Department } from "../model/Department.model"

export class Employee {
  public Id: number;
  public Name: string;
  public DepartmentId: number;
  public Department: Department;
  public DepartmentName: string;

  constructor(empId: number, name: string, depId: number, dep: Department, deptName:string) {
    this.Id = empId;
    this.Name = name;
    this.DepartmentId = depId;
    this.Department = dep;
    this.DepartmentName = deptName;
  }

  static deserialize(employeeResponse: any): Employee {
    return new Employee(employeeResponse.Id, employeeResponse.Name, employeeResponse.DepartmentId,
      employeeResponse.Department, employeeResponse.Department.Name);
  }
}
