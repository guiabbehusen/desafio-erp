import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

export function Contato() {
    const navigate = useNavigate();

    useEffect(() => {
    }, []);

    return (
        <div className="container mt-5">
            <h2 className="text-center mb-4 text-primary">Entre em Contato com a Cad+</h2>

            <p className="text-center text-muted">
                A Cad+ é uma empresa inovadora especializada em ERP para hospitais, oferecendo soluções completas com módulos flexíveis para gestão de dados e processos hospitalares.
            </p>

            <p className="text-center text-muted mb-4">
                O módulo de cadastro de usuários é comum em todas as versões do ERP, permitindo uma administração eficiente e segura das informações.
            </p>

            <h3 className="mt-5 text-center text-success">Fale Conosco</h3>
            <div className="text-center border p-4 rounded shadow-sm">
                <p><strong>Email:</strong> <a href="mailto:contato@cadmais.com.br" className="text-decoration-none text-primary">contato@cadmais.com.br</a></p>
                <p><strong>Telefone:</strong> <a href="tel:+5561912345678" className="text-decoration-none text-primary">(61) 91234-5678</a></p>
                <p><strong>Endereço:</strong> Rua Exemplo, 123, Brasília - DF, Brasil</p>
            </div>

            <h4 className="mt-5 text-center text-info">Ou, caso prefira, utilize nossas redes sociais:</h4>
            <div className="text-center">
                <p><strong>Facebook:</strong> <a href="https://www.facebook.com/cadmais" target="_blank" rel="noopener noreferrer" className="text-decoration-none text-primary">Cad+ no Facebook</a></p>
                <p><strong>Instagram:</strong> <a href="https://www.instagram.com/cadmais" target="_blank" rel="noopener noreferrer" className="text-decoration-none text-primary">@cadmais</a></p>
            </div>

            <div className="text-center mt-5">
                <p className="text-muted">Estamos sempre prontos para ouvir você! Entre em contato e descubra como podemos ajudá-lo com soluções inovadoras para o seu hospital.</p>
            </div>

            <div className="text-center mt-4">
                <button
                    onClick={() => navigate(-1)}
                    className="btn btn-secondary"
                >
                    Voltar
                </button>
            </div>
        </div>
    );
}
