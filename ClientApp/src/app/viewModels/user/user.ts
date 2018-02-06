export interface RegisterInfo {
    email: string;
    password: string;
}

export interface UserPassword {
    id: number;
    userId: number;
    password: string;
    passwordCreated: Date;
    active: boolean;
}

export interface ViewUser {
    userId: number;
    email: string;
    firstName: string;
    lastName: string;
    isFCUser: boolean;
    lastLogin: Date;
    note: string;
    active: boolean;
    fullName: string;
}