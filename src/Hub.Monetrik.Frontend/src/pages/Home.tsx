import { CircleDollarSign } from "lucide-react";
import "./style.css";
import Navbar from "../components/Navbar";

function Home() {
  return (
    <>
      <Navbar />
      <div className="container">
        <h1 className="titulo-home">
          Dashboard Hub Monetrik{" "}
          <CircleDollarSign className="icon" size={20} strokeWidth={2.25} />
        </h1>
        <p className="descricao-home">
          Bem-vindo ao Hub Monetrik, onde você pode gerenciar suas finanças de
          forma simples e eficiente.
        </p>
        <p className="descricao-home">
          Explore as funcionalidades e comece a otimizar sua gestão financeira.
        </p>
      </div>
    </>
  );
}

export default Home;
