export interface VeiculoAtualizarDto {
  id?: number;              
  marca: string;            
  modelo: string;           
  ano: number;            
  placa: string;           
  km?: number;              
  cor: string;              
  preco: number;            
  opcionais?: string[];     
  fotos?: File[];          
}

