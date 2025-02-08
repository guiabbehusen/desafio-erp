import React from "react";
import { api } from "../lib/axios";

export const Login: React.FC = () => {
    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        console.log("Formulário enviado!");
    };

    return (
        <div className="container mt-5">
            <h1 className="text-center">Bem-vindo de volta</h1>
            <form className="mt-4" onSubmit={handleSubmit}>
                <div className="mb-3">
                    <label htmlFor="email" className="form-label">E-mail</label>
                    <input type="email" className="form-control" id="email" placeholder="Digite seu e-mail" required />
                </div>
                <div className="mb-3">
                    <label htmlFor="password" className="form-label">Senha</label>
                    <input type="password" className="form-control" id="password" placeholder="Digite sua senha" required />
                </div>
                <button type="submit" className="btn btn-primary w-100">Entrar</button>
            </form>
            <p className="mt-3 text-center">
                Não possui uma conta? <a href="/cadastro">Cadastrar</a>
            </p>
            <p className="text-center">
                <a href="/contato">Entrar em contato</a>
            </p>
        </div>
    );
};
