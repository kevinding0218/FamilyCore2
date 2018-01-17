export interface KeyValuePairInfo {
    id: number;
    name: string;
}

export interface SaveEntreeDetail {
    keyValuePairInfo: KeyValuePairInfo;
    addedOn: Date;
    addedById: number;
    updatedOn: Date;
    lastUpdatedById: number;
    detailType: string;
    note: string;
}

export interface GridEntreeDetail {
    keyValuePairInfo: KeyValuePairInfo;
    addedOn: Date;
    addedByUserName: string;
    numberOfEntreeIncluded: number;
    lastUpdatedByOn: Date;
    note: string;
}