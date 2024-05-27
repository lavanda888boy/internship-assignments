import { createContext, useState, ReactNode } from "react";

interface PageContextProps {
  pageName: string;
  setPageName: (name: string) => void;
}

export const PageContext = createContext<PageContextProps | undefined>(
  undefined
);

interface PageContextProviderProps {
  children: ReactNode;
}

export const PageContextProvider = ({ children }: PageContextProviderProps) => {
  const [pageName, setPageName] = useState("");

  return (
    <PageContext.Provider value={{ pageName, setPageName }}>
      {children}
    </PageContext.Provider>
  );
};
