const _blankRoute = [
  {
    path: "/login",
    loader: () => import("src/view/Auth/LoginPage"),
  },
].filter(Boolean);

const _CommonRoute = [
  {
    path: "/",
    loader: () => import("src/view/BackOffice/Home/Home"),
    exact: true,
  },
].filter(Boolean);

export default { _blankRoute, _CommonRoute };
