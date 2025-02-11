export interface EnderecoProps {
    cep: string;
    rua: string;
    numero: string;
    bairro: string;
    cidade: string;
    estado: string;
}

export interface UsuarioProps {
    cpf: string;
    nome: string;
    email: string;
    telefone: string;
    login: string;
    enderecos: EnderecoProps[];
}

export interface EditarUsuarioProps {
    nome: string;
    email: string;
    telefone: string;
}