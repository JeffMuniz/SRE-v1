
import styled from 'styled-components';
import {Checkbox} from '~/components';
import {Clickable, PageSubtitle} from '~/styled';

const {PUBLIC_URL} = process.env;

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
`;

export const OCPageSubtitle = styled(PageSubtitle)`
  margin-bottom: 5px;
`;

export const FindAffiliationCode = styled(Clickable)`
  display: block;
  font-size: 14px;
  font-weight: 500;
  margin-bottom: 20px;
`;

export const OCCheckbox = styled(Checkbox).attrs({
  checked: true,
  onChange: () => { },
})`
  align-self: flex-start;
  height: 55px;
  margin-bottom: 10px;
  
  & > * {
    align-self: center;
  }

  & > div {
    margin-right: 10px;
  }
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
  src: `${PUBLIC_URL}/images/cielo-logo.png`,
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

