export interface SaveCurrentOrder {
    id: number;
    startDate: Date;
    endDate: Date;
    addedOn: Date;
    addedById: number;
    lastUpdatedByOn: Date;
    lastUpdatedById: number;
    note: string;
    mappingEntreeIdsWithCurrentOrder: number[];
}