
import {Fragment} from 'react';
import styled, {css} from 'styled-components';
import {ErrorModal, Input} from '~/components';
import {mqTablet} from '~/styled';

export const Wrapper = Fragment;

export const TabletInlineWrapper = styled.div`
  ${mqTablet(css`
    display: flex;
    width: 100%;
  `)}
`;

export const InlineWrapper = styled.div`
  display: flex;
`;

export const inputMarginRightBlock = css`
  input {
    padding-right: 70px;
  }
  img {
    margin-left: -60px;
  }
`;

export const StreetInput = styled(Input)`
  margin-bottom: 20px;

  ${mqTablet(css`
    flex-grow: 1;
  `)}
`;

export const NumberInput = styled(StreetInput)`
  align-self: flex-start;
  margin-bottom: 0;
  width: 170px;
  ${inputMarginRightBlock}

  ${mqTablet(css`
    flex-grow: unset;
    min-width: 150px;
  `)}
`;

export const AdditionalInfoInput = styled(StreetInput)`
  flex-grow: 1;
`;

export const ZipCodeInput = styled(StreetInput)`
  ${mqTablet(css`
    flex-grow: unset;
    min-width: 230px;
    ${inputMarginRightBlock}  
  `)}
`;

export const NeighborhoodInput = styled(StreetInput)``;

export const CityInput = styled(StreetInput)``;

export const StateInput = styled(StreetInput)`
  margin-bottom: 50px;

  ${mqTablet(css`
    max-width: 100px;
  `)}
`;

export const FErrorModal = styled(ErrorModal)``;
