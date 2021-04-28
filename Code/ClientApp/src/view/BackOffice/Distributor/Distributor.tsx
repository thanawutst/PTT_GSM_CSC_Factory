import React, { useEffect, useState } from "react";
import DistributorForm from "src/view/BackOffice/Distributor/DistributorForm";

const Distributor = () => {
  let params = new URLSearchParams(window.location.search);
  let ID = params.get("ID");

  return <DistributorForm nDistID={ID}></DistributorForm>;
};

export default Distributor;
