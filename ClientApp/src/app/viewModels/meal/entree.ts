export interface SaveEntree {
    id: number;
    name: string;
    stapleFoodId: number;
    entreeCatagoryId: number;
    entreeStyleId: number,
    currentRank: number;
    addedOn: Date;
    addedById: number;
    updatedOn: Date;
    lastUpdatedById: number;
    note: string;
    entreeDetailIds: number[];
    vegetables: string;
    meats: string;
    seafoods: string;
    sauces: string;
    ingredients: string;
}