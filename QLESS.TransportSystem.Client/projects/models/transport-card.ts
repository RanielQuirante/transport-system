export interface TransportCard {
    id?: number;
    loanAmount?: number;
    isDiscounted?: boolean;
    isInside?: boolean;
    seniorCitizenId?: string;
    pwdId?: string;
    createdDate?: Date;
    expirationDate?: Date;
    entryCount?: number;
}
