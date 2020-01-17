import { Deserializable } from "../model/deserializable.model";

export class Department implements Deserializable {
    public Id: number;
    public Name: string;

    constructor(id: number, name: string) {
        this.Id = id;
        this.Name = name;
    }  

    static deserialize(deptResponse: any): Department {
        return new Department(deptResponse.Id, deptResponse.Name);
    }
   
}
