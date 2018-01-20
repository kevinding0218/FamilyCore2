export interface SaveEntree {
    id: number;
    name: string;
    stapleFoodId: number;
    entreeCatagoryId: number;
    entreeStyleId: number,
    currentRank: number;
    addedOn: Date;
    addedById: number;
    lastUpdatedByOn: Date;
    lastUpdatedById: number;
    note: string;
    entreeDetails: EntreeDetailMappingResource[];
}

export interface EntreeDetailMappingResource {
    entreeDetailId: number;
    name: string;
    quantity: number;
    entreeDetailTypeName: string;
    displayMode: boolean;
}

export interface SimilarEntreeInputObj {
    stapleFoodId: number;
    entreeName: string;
    entreeDetailIdList: string;
}