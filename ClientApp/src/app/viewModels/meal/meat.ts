export interface KeyValuePairInfo {
    id: number;
    name: string;
}

export interface SaveMeat {
    keyValuePairInfo: KeyValuePairInfo;
    addedOn: Date;
    addedByUserId: number;
    updatedOn: Date;
    lastUpdatedByUserId: number;
    note: string;
}

export interface GridMeat {
    keyValuePairInfo: KeyValuePairInfo;
    addedOn: Date;
    addedByUserName: string;
    numberOfEntreeIncluded: number;
    lastUpdatedByOn: Date;
    stapleFood: string;
    note: string;
}