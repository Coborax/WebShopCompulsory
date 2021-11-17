import {UserDto} from "./user.dto";

export interface TokenDto {
  jwtToken: string;
  user: UserDto;
}
