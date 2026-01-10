mergeInto(LibraryManager.library, {
  IsAndroid: function() {
    var userAgent = navigator.userAgent.toLowerCase();
    return (/android/.test(userAgent));
  }
});