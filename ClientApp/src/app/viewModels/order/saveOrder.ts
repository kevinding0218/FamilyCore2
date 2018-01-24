export interface SaveInitialOrder {
    id: number;
    startDate: Date;
    endDate: Date;
    addedOn: Date;
    addedById: number;
    lastUpdatedByOn: Date;
    lastUpdatedById: number;
    note: string;
    entreeOrderMappingsWithCurrentOrder: EntreeOrderMapping[];
}

export interface EntreeOrderMapping {
    entreeId: number;
    count: number;
}