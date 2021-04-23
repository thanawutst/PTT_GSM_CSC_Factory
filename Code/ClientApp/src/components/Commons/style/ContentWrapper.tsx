import styled from "@material-ui/core/styles/styled";

const ContentWrapper = styled("div")({
  marginTop: "16px",
  padding: "24px",
  backgroundColor: "#fff",
  width: "100%",
  border: "1px solid #dee2e6 !important",
  borderRadius: "5px",
});
export const FormWrapper = styled("div")({
  paddingTop: 0,
  paddingBottom: 0,
});

export const FormButtons = styled("div")({
  paddingTop: "16px",
  display: "flex",

  "& > *": {
    marginRight: "8px",
    marginBottom: "8px",
  },
});

export default ContentWrapper;
