
import styled, {css} from 'styled-components';
import {Checkbox} from '~/components';
import {
 PageSubtitle, PrimaryButton, Spacer, mqTablet,
} from '~/styled';

const {PUBLIC_URL} = process.env;

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
`;

export const OPageSubtitle = styled(PageSubtitle)`
  margin-bottom: 20px;
`;

export const Form = styled.form`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
`;

export const CheckboxesWrapper = styled.div`
  align-self: center;
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  margin-bottom: 40px;
  user-select: none;
  width: fit-content;

  ${mqTablet(css`
    align-self: flex-start;
    flex-direction: row;
    flex-grow: unset;
    flex-wrap: wrap;
    justify-content: flex-start;
  `)}
`;

export const ACheckbox = styled(Checkbox)`
  align-self: flex-start;
  height: 45px;
  margin-bottom: 20px;

  &:last-child {
    margin-bottom: 0;
  }
  
  & > * {
    align-self: center;
  }

  & > div {
    margin-right: 10px;
  }

  ${mqTablet(css`
    align-self: flex-start;
    margin-right: 20px;
    width: 200px;
  `)}
`;

export const GetnetLogo = styled.img.attrs({
  src: `${PUBLIC_URL}/images/getnet-logo.png`,
})`
  align-self: center;
  display: block;
  max-height: 44px;
`;

export const SafrapayLogo = styled(GetnetLogo).attrs({
  src: `${PUBLIC_URL}/images/safrapay-logo.png`,
})`
  max-height: 38px;
`;

export const CieloLogo = styled.img.attrs({
  src: `${PUBLIC_URL}/images/macna-logo.png`,
})`
  margin-top: -8px;
  max-height: 45px;
`;

export const RedeLogo = styled.img.attrs({
  src: `${PUBLIC_URL}/images/rede-logo.png`,
})`
  max-height: 38px;
  margin-top: -15px;
`;

export const StoneLogo = styled.img.attrs({
  src: `${PUBLIC_URL}/images/stone-logo.png`,
})`
  margin-top: -15px;
  max-height: 54px;
`;

export const PagSeguroLogo = styled.img.attrs({
  src: `${PUBLIC_URL}/images/pag-seguro-logo.png`,
})`
  max-height: 37px;
`;

export const OSpacer = styled(Spacer)``;

export const ContinueButton = styled(PrimaryButton).attrs({
  type: 'submit',
})`
  ${mqTablet(css`
    align-self: flex-end;
  `)}
`;
