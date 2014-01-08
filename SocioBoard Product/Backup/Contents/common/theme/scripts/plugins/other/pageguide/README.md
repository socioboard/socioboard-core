pageguide.js
============

An interactive guide for web page elements using jQuery and CSS3.

## How-to:
1. Add references in your code to pageguide.js, jQuery & pageguide.css
2. Add a document ready callback to setup the page guide
3. Add a simple `<ul>` to the bottom of the pages you want the pageguide to appear on.
4. Customize the page guide tour title.

## An example:

### Step 1 - Add pageguide.js 

Add `<script src="pageguide.js"></script>` to the bottom of your html document, right before your closing `</body>` tag.

We provide both the standard js as well as a minified version.

### Step 2 - Add pageguide.css

Add `<link rel="stylesheet" href="stylesheets/pageguide.css">` to the header of your html document.

We provide a css file as well as a minified version. Alternatively, we use <a href="http://lesscss.org/" target="_blank">LESS CSS</a> at Tracelytics, so we provide that as well. 

### Step 3 - Add setup code

Add the following block of JavaScript to your html document:

```$(document).ready(function() {
    tl.pg.init({ /* optional preferences go here */ });
});```

### Step 4 - Choose the elements that you want included in the page guide.
pageguide.js matches the first occurrence of the selector you specify in the `<ul>` you put on your pages in the next step.

### Step 5 - Add the pageguide.js `<ul>` near the bottom of your pages.

    <ul id="tlyPageGuide" data-tourtitle="REPLACE THIS WITH A TITLE">
      <li class="tlypageguide_left" data-tourtarget=".first_element_to_target">
        <div>
          Here is the first item description. The number will appear to the left of the element.
        </div>
      </li>
      <li class="tlypageguide_right" data-tourtarget="#second_element_to_target">
        <div>
          Here is the second item description. The number will appear to the right of the element.
        </div>
      </li>
      <li class="tlypageguide_top" data-tourtarget=".third_element_to_target > div.is_here">
        <div>
          Here is the third item description. The number will appear above the element.
        </div>
      </li>
      <li class="tlypageguide_bottom" data-tourtarget="#fourth_element_to_target">
        <div>
          Here is the fourth item description. The number will appear below the element.
        </div>
      </li>
    </ul>


## See it IRL:
* http://tracelytics.github.com/pageguide

## Contribute
Bugfix?  Cool new feature?  Alternate style?  Send us a pull request!
