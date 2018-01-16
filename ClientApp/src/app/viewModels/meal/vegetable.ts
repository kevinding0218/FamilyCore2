export interface KeyValuePairInfo {
    id: number;
    name: string;
}

export interface SaveVegetable {
    keyValuePairInfo: KeyValuePairInfo;
    addedOn: Date;
    addedByUserId: number;
    updatedOn: Date;
    lastUpdatedByUserId: number;
    note: string;
}

export interface GridVegetable {
    keyValuePairInfo: KeyValuePairInfo;
    addedOn: Date;
    addedByUserName: string;
    numberOfEntreeIncluded: number;
    lastUpdatedByOn: Date;
    stapleFood: string;
    note: string;
}