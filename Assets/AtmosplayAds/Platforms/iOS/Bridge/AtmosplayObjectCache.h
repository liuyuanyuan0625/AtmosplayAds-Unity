#import <Foundation/Foundation.h>

@interface AtmosplayObjectCache : NSObject

+ (instancetype)sharedInstance;

@property(nonatomic, strong) NSMutableDictionary *references;

@end

@interface NSObject (AtmosplayOwnershipAdditions)

- (NSString *)atmosplayAds_referenceKey;

@end
