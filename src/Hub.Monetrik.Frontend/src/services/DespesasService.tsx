import axios from "axios";
import type {
  ApiResponse,
  BuscarDespesasResponse,
} from "../interfaces/IHubMonetrikApi";
import api from "./HubMonetrikService";

export class DespesasService {
  private readonly baseUrl = "/Despesas";

  async buscarDespesas(): Promise<BuscarDespesasResponse> {
    try {
      console.log("🔄 Iniciando busca de despesas...");

      const response = await api.get<ApiResponse<BuscarDespesasResponse>>(
        `${this.baseUrl}/buscar-despesas`
      );

      console.log("📦 Response completa:", response);
      console.log("✅ Response data:", response.data);

      if (!response.data.success) {
        const errorMessage =
          response.data.errors?.[0]?.message || "Erro ao buscar despesas";
        console.error("❌ API retornou sucesso = false:", errorMessage);
        throw new Error(errorMessage);
      }

      console.log("🎉 Despesas carregadas com sucesso:", response.data.data);
      return response.data.data;
    } catch (error) {
      console.error("💥 Erro completo:", error);

      if (axios.isAxiosError(error)) {
        console.error("🔍 Detalhes do erro Axios:");
        console.error("- Status:", error.response?.status);
        console.error("- StatusText:", error.response?.statusText);
        console.error("- Data:", error.response?.data);
        console.error("- Headers:", error.response?.headers);

        if (error.response?.status === 400) {
          throw new Error("Nenhuma despesa encontrada");
        }

        if (error.code === "ERR_NETWORK") {
          throw new Error("Erro de rede - Verifique se a API está rodando");
        }

        throw new Error(
          `Erro na API: ${error.response?.status} - ${error.message}`
        );
      }

      throw new Error("Erro desconhecido ao buscar despesas");
    }
  }

  // ...resto do código...
}

export const despesasService = new DespesasService();
