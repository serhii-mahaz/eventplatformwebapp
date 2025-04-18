"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Button } from "@/components/ui/button";
import { login } from "@/lib/api/auth";
import { tokenStorage } from "@/lib/services/tokenStorage";

export default function LoginPage() {
  const router = useRouter();
  const [error, setError] = useState("");

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const formData = new FormData(e.currentTarget);
    const email = formData.get("email") as string;
    const password = formData.get("password") as string;

    try {
      const { accessToken } = await login(email, password);
      tokenStorage.setToken(accessToken);
      router.push("/");
    } catch (err: any) {
      setError(err.message || "Помилка входу");
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-background">
      <div className="w-full max-w-md p-8 border border-border rounded-xl shadow-md">
        <h1 className="text-2xl font-semibold mb-6 text-center">Вхід</h1>
        <form className="space-y-4" onSubmit={handleSubmit}>
          {error && <p className="text-red-500 text-sm">{error}</p>}
          <div className="space-y-2">
            <Label htmlFor="email">Email</Label>
            <Input name="email" id="email" type="email" placeholder="you@example.com" required />
          </div>
          <div className="space-y-2">
            <Label htmlFor="password">Пароль</Label>
            <Input name="password" id="password" type="password" required />
          </div>
          <Button type="submit" className="w-full">
            Увійти
          </Button>
        </form>
      </div>
    </div>
  );
}
