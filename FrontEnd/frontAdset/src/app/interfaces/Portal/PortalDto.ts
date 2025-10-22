import { PacoteDto } from "../Pacote/PacoteDto";

export interface PortalDto {
  id: number;
  nome: string;
  pacotes: PacoteDto[];
}