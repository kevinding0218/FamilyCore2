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
    note: string;
}

export interface OrderProcessInfo {
    id: number;
    startDate: Date;
    endDate: Date;
    addedOn: Date;
    addedById: number;
    lastUpdatedByOn: Date;
    lastUpdatedById: number;
    note: string;
    entreeInfoList: OrderProcessingSingleEntree[];
}

export interface OrderProcessingSingleEntree {
    orderId: number;
    entreeId: number;
    style: string;
    catagory: string;
    entreeName: string;
    entreeCount: number;
    note: string;
    entreeImgUrl: string;
}

export interface OrderEntreeDetailInfo {
    entreeDetailName: string;
    entreeDetailQty: number;
    entreeDetailTypeName: string;
    stapleFood: string;
}