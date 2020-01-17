import { Deserializable } from "./deserializable.model";

export class EnumModel implements Deserializable {
    public Id: number;
    public NameAr: string;
    public NameEn: string;

    constructor(id: number, nameAr: string, nameEn: string) {
        this.Id = id;
        this.NameAr = nameAr;
        this.NameEn = nameEn;
    }


    static deserialize(enumResponse: any): EnumModel {
        return new EnumModel(enumResponse.Id, enumResponse.NameAr, enumResponse.NameEn);
    }
}
