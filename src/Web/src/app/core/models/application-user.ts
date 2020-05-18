import { FullName } from "./full-name";
import { Address } from "./address";

export class ApplicationUser {
    id: string;
    address: Address;
    name: FullName;
    email: string;
}
