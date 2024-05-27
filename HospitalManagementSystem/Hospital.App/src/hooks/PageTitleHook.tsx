import { useContext, useEffect } from "react";
import { PageContext } from "../context/PageContext";

const usePageTitle = (title: string) => {
  const pageContextProps = useContext(PageContext);

  useEffect(() => {
    if (pageContextProps) {
      pageContextProps.setPageName(title);
      document.title = `Hospital | ${title}`;
    }
  }, [title, pageContextProps]);
};

export default usePageTitle;
