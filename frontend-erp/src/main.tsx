import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import 'bootstrap/dist/css/bootstrap.min.css';
import { App } from './App';
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { Cadastro } from './pages/Cadastro';
import { Home } from './pages/Home';
import { AlterarDados } from './pages/AlterarDados';
import { AlterarEndereco } from './pages/AlterarEndereco';
import { Contato } from './pages/Contato';

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
  },
  {
    path: "/cadastro",
    element: <Cadastro />,
  },
  {
    path: "/contato",
    element: <Contato />,
  },
  {
    path: "/home",
    element: <Home />,
  },
  {
    path: "/alterar_dados",
    element: <AlterarDados />,
  },
  {
    path: "/alterar_endereco",
    element: <AlterarEndereco />,
  },
]);

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>,
);
