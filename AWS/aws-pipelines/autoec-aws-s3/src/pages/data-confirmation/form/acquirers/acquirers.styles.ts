
import styled, {css} from 'styled-components';
import {Checkbox} from '~/components';
import {
 BaseLabel, Clickable, mqTablet,
} from '~/styled';

const {PUBLIC_URL} = process.env;

export const Wrapper = styled.div`
  margin-bottom: 30px;
`;

export const EditLabel = styled(Clickable)``;

export const TabletInlineWrapper = styled.div`
  margin-top: 20px;
  ${mqTablet(css`
    display: flex;
    flex-wrap: wrap; 
  `)}
`;

export const AcquirerWrapper = styled.div`
  margin-bottom: 20px;
  &:last-child {
    margin-bottom: 0;
  }

  ${mqTablet(css`
    width: 250px;
  `)}
`;

export const ACheckbox = styled(Checkbox)`
  * {
    align-self: center;
  }
  & > div {
    margin-right: 5px;
  }
`;

export const DetailWrapper = styled.div`
  padding-left: 35px;
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

export const DetailLabel = styled(BaseLabel)`
  display: block;
`;
