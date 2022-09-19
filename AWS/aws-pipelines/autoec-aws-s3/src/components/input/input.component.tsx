import {
 FC, useEffect, useRef, useState,
} from 'react';
import AutocompleteOptions from './autocomplete-options/autocomplete-options';
import {
 EditIcon, ErrorMessage, HeightWrapper, InlineWrapper, Label, TextInput, Wrapper,
} from './input.component.styles';

const Input: FC<Input.Props> = ({
  autocompleteOptions,
  autoFocus = false,
  className,
  disabled,
  errorMessage,
  id,
  label,
  maxLength,
  name,
  onAutocompleteOptionSelect,
  onBlur = () => { },
  onChange = () => { },
  onFocus = () => { },
  placeholder,
  showEditIndicator,
  type = 'text',
  value,
}) => {

  const inputRef = useRef<HTMLInputElement>(null);

  const [ state, setState ] = useState<Input.State>({
    isFocused: false,
  });

  useEffect(() => {
    setTimeout(() => {
      if(!autoFocus || !inputRef.current) return;
      inputRef.current.focus();
      inputRef.current.setSelectionRange(0, 0);
    }, 1000);
  }, [ autoFocus, inputRef ]);

  const handleEditIconClick = () => {
    inputRef.current.focus();
  };

  return (
    <Wrapper className={className}>
      {label && <Label htmlFor={id}>{label}</Label>}
      <HeightWrapper>
        <InlineWrapper>
          <TextInput
            ref={inputRef}
            id={id}
            maxLength={maxLength}
            name={name || id}
            onBlur={event => {
              setState(prev => ({...prev, isFocused: false}));
              onBlur(event);
            }}
            onFocus={event => {
              setState(prev => ({...prev, isFocused: true}));
              onFocus(event);
            }}
            onChange={onChange}
            placeholder={placeholder}
            type={type}
            value={value}
            disabled={disabled}
          />
          {showEditIndicator && <EditIcon onClick={handleEditIconClick} />}
        </InlineWrapper>
        {autocompleteOptions?.length > 0 && (
          <AutocompleteOptions
            inputRef={inputRef}
            onSelect={onAutocompleteOptionSelect}
            options={autocompleteOptions}
            searchString={value}
            show={state.isFocused}
          />
        )}
      </HeightWrapper>
      <ErrorMessage>
        {errorMessage}
      </ErrorMessage>
    </Wrapper>
  );
};

export default Input;
