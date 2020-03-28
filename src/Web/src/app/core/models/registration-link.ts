import { ApplicationUser } from "./application-user";

export class RegistrationLink {
  id: string;
  used: boolean;
  applicationUser: ApplicationUser;
}
