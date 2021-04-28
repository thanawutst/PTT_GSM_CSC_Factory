import Layout_MP_Back from "src/layout/CommonLayout/Layout_MP_Back";
import Home from "src/view/BackOffice/Home/Home";
import TestComponentsForm from "src/view/BackOffice/TestComponents/TestComponentsForm";
import Distributor from "src/view/BackOffice/Distributor/Distributor";
import DistributorList from "src/view/BackOffice/Distributor/DistributorList";

const routes = [
  {
    exact: true,
    path: "/",
    component: Home,
    layout: Layout_MP_Back,
  },
  {
    exact: true,
    path: "/b_test_comp",
    component: TestComponentsForm,
    layout: Layout_MP_Back,
  },
  {
    exact: true,
    path: "/b_distributor",
    component: Distributor,
    layout: Layout_MP_Back,
  },
  {
    exact: true,
    path: "/b_distributor/list",
    component: DistributorList,
    layout: Layout_MP_Back,
  },
];

export default routes;
