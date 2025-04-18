import api from "./axios";

export async function login(email: string, password: string) {
    try {
      const response = await api.post("/auth/login", {
        email,
        password,
      });
  
      return response.data; // очікується { token: string }
    } catch (error: any) {
      if (error.response && error.response.data?.message) {
        throw new Error(error.response.data.message);
      }
      throw new Error("Помилка під час входу");
    }
  }
